
import os
import requests

import numpy as np
import pandas as pd

from obspy import read
from obspy.core.utcdatetime import UTCDateTime

from util import linear_interpolation

df = pd.read_csv("C:/Users/sibun/Documents/dev/moonquake/moonquakes.csv")

failures = []

for i, row in df.iterrows():
    name = f"{row['Type']}_{row['Subtype']}"
    url = row["Url"]

    print(f"Generating audio data for {name} {i}")

    starttime = UTCDateTime(year=row["Year"], month=row["Month"], day=row["Day"], hour=row["Hour"], minute=row["Minute"])
    if row['Subtype'] == "landing":
        starttime -= 1800 # look half an hour before / after landing? obviously LM11 won't work!
    endtime = starttime + 3600 # one hour

    if not os.path.exists("audio"):
        os.mkdir("audio")
    
    if not os.path.exists("plots"):
        os.mkdir("plots")

    if requests.head(url).status_code == 200:
        stream = read(url)

        try:
            stream = stream.trim(starttime, endtime, pad=True, nearest_sample=True, fill_value=0.0)
            #print(stream[0].stats)

            for tr in stream:
                # interpolate across the gaps of one sample 
                linear_interpolation(tr,interpolation_limit=1)
                # we cannot write out unless we do this
                # https://github.com/obspy/obspy/issues/703
                if isinstance(tr.data, np.ma.masked_array):
                    tr.data = tr.data.filled()
            stream.merge(method=0, fill_value='interpolate', interpolation_samples=0)

            # rescale is needed otherwise we get a flat wave in Audacity
            # https://github.com/obspy/obspy/issues/791
            stream.write(f"audio/audio{i}.wav", format="WAV", framerate=7000, rescale=True)
            stream.plot(outfile=f"plots/plot{i}.png")

        except:
            print(f"Cannot generate audio for {name}{i}!")
            failures.append(f"{name}{i}")

print(failures)
