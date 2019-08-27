
#!/usr/bin/env python
# coding: utf-8

# In[ ]:

from string import ascii_uppercase
import json
import os
import pickle
import argparse

cwd = os.getcwd()

def dir_path(string):
    if os.path.isdir(string):
        return string
    else:
        raise NotADirectoryError(string)

parser = argparse.ArgumentParser(description='Process some integers.')
parser.add_argument('-p', '--path', type=dir_path)
args = parser.parse_args() #this is the variable that stores the inputs from the UI
path =(args.path)

addelementtopath = True;

if (os.path.isfile(path +'config') == False) or (addelementtopath == True):
    dict =  {'rows': 0, 'cols': 0, 'shape': 0, 'brand': 0, 'brandId': 0, 'link': 0, 'displayName': 0, 'displayCategory': 0, 'displayVolumeUnits': 0, 'tags': 0, 'xDimension': 0, 'yDimension': 0, 'zDimension': 0, 'coloffset': 0, 'rowoffset': 0, 'colspace': 0,'rowspace': 0, 'diameter': 0, 'depth': 0, 'totalLiquidVolume': 0, 'loadName':0}
    with open(path + 'config', 'wb') as f:
        pickle.dump(dict, f);
else:
    with open(path +'config', 'rb') as f:
        dict = pickle.load(f);
        f.close();
    
import itertools
line = open(path + "labware_val.csv").read().split("\n")
line.remove("")
for a, b in zip(dict, line):
    if b.isdigit() == True:
        dict[a] =float(b)
    else:
        dict[a]=b
        
rows = int(dict["rows"])
cols = int(dict["cols"])

columns = []
ordering =[]
for c in range(1, cols+1):
    for r in range(rows):
        columns.append(ascii_uppercase[r]+str(c))
    ordering.append(columns)
    columns =[]
    
wellstotal = {}
for (i,row) in enumerate(ordering):
  for (j,value) in enumerate(row):
    welltext = {value:{'depth':dict["zDimension"]-dict["depth"],'shape':dict["shape"],'diameter':dict["diameter"],'totalLiquidVolume':dict["totalLiquidVolume"],'x':dict["coloffset"]+i*dict["colspace"],'y':(dict["rowoffset"]+dict["rowspace"]*rows)-j*dict["rowspace"],'z':dict["depth"]}}
    wellstotal.update(welltext)


parameters = {"format":"96Standard","isTiprack":False,"isMagneticModuleCompatible":False,"loadName":dict["loadName"]}
data ={'ordering':ordering,'brand':{'brand':dict["brand"],'brandId':dict["brandId"],'links':dict["link"]},'metadata':{'displayName':dict["displayName"],'displayCategory':dict["displayCategory"],'displayVolumeUnits':dict["displayVolumeUnits"],'tags':dict["tags"]},'dimensions':{'xDimension':dict["xDimension"],'yDimension':dict["yDimension"],'zDimension':dict["zDimension"]},'cornerOffsetFromSlot':{'x':0,'y':0,'z':0},'wells':wellstotal,"parameters":parameters,"namespace":"opentrons","version":1,"schemaVersion":2}    
jstr = json.dumps(data, indent=4)



end_path = (path + dict["loadName"])
if not os.path.exists(end_path): 
    os.mkdir(end_path)
with open(end_path+'/1.json', 'w') as json_file:
    json.dump(data, json_file, indent=4)
    json_file.close()
with open(end_path +'/flag', 'wb') as f:
    pickle.dump(True, f);
    f.close();

exit()