import requests

stations = [ 11, 12, 14, 15, 16 ]

def find_station_url(year, julday):

    for station in stations:
        #print(f"checking station {station}")
        url = f"https://pds-geosciences.wustl.edu/lunar/urn-nasa-pds-apollo_pse/data/xa/continuous_waveform/s{station}/{int(year)}/{int(julday)}/xa.s{station}.00.mh1.{int(year)}.{int(julday)}.0.mseed"
        if requests.head(url).status_code == 200:
            return(station, url)
        #time.sleep(10)

    # it's probably in station 12
    station = 12
    url = f"https://pds-geosciences.wustl.edu/lunar/urn-nasa-pds-apollo_pse/data/xa/continuous_waveform/s{station}/{int(year)}/{int(julday)}/xa.s{station}.00.mh1.{int(year)}.{int(julday)}.0.mseed"   
    return(station, url)