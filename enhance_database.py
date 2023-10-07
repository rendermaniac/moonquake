
import time
import requests
import pandas as pd
from obspy.core.utcdatetime import UTCDateTime

stations = [ 11, 12, 14, 15, 16 ]

def find_station_url(year, julday):

    for station in stations:
        print(f"checking station {station}")
        url = f"https://pds-geosciences.wustl.edu/lunar/urn-nasa-pds-apollo_pse/data/xa/continuous_waveform/s{station}/{int(year)}/{int(julday)}/xa.s{station}.00.mh1.{int(year)}.{int(julday)}.0.mseed"
        if requests.head(url).status_code == 200:
            return(station, url)
        #time.sleep(10)

    # it's probably in station 12
    station = 12
    url = f"https://pds-geosciences.wustl.edu/lunar/urn-nasa-pds-apollo_pse/data/xa/continuous_waveform/s{station}/{int(year)}/{int(julday)}/xa.s{station}.00.mh1.{int(year)}.{int(julday)}.0.mseed"   
    return(station, url)

df = pd.read_csv("NASA/lunar/urn-nasa-pds-apollo_seismic_event_catalog/data/gagnepian_2006_catalog.csv")

# d = df['Date'].iloc[0]
# print(d)
# print(str(d)[8:10])

for i, row in df.iterrows():
    date_raw = str(row["Date"])
    year = int("19" + date_raw[0:2])
    month = int(date_raw[2:4])
    day = int(date_raw[4:6])
    hour = int(date_raw[6:8])
    minute = int(date_raw[8:10])
    date = UTCDateTime(year=year, month=month, day=day, hour=hour, minute=minute)
    station, url = find_station_url(date.year, date.julday)
    df.at[i,'Year'] = date.year
    df.at[i,'Month'] = date.month
    df.at[i,'Day'] = date.day
    df.at[i,'Julday'] = date.julday
    df.at[i,'Hour'] = date.hour
    df.at[i,'Minute'] = date.minute

    df.at[i,'Station'] = station
    df.at[i,'Url'] = url

    print(f"wrote line {i}")

print(df.head())

df.to_csv('moonquakes_raw.csv')