
import time
import requests
import pandas as pd
from obspy.core.utcdatetime import UTCDateTime

from psedata import find_station_url

df = pd.read_csv("NASA/lunar/urn-nasa-pds-apollo_seismic_event_catalog/data/gagnepian_2006_catalog.csv")

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