import pandas as pd
from obspy.core.utcdatetime import UTCDateTime
from psedata import find_station_url

df = pd.read_csv("C:/Users/sibun/Documents/dev/moonquake/NASA/apollo_landing_sites.csv")

for i, row in df.iterrows():
    name = f"{row['Type']}_{row['Subtype']}"

    date = UTCDateTime(year=row["Year"], month=row["Month"], day=row["Day"], hour=row["Hour"], minute=row["Minute"])
    station, url = find_station_url(date.year, date.julday)

    print(f"{row['Type']} {date.julday} {station} {url}")