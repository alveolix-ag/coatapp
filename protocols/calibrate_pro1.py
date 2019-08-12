#!/usr/bin/env python
# coding: utf-8

# In[ ]:


from opentrons import labware, instruments, robot, containers #Libraries from Opentron API
import signal
import sys 
import pickle #This is used for storing the variable of the curent tiprack well
import subprocess #Allows to run subprocess, video recording
import argparse
import re

import functools
import logging
from copy import copy

from opentrons.util import calibration_functions


# metadata
#Information about the protocol 
metadata = {
    'protocolName': 'Calibrate',
    'author': 'Daniel Nakhaee-Zadeh Gutierrez',
    'description': 'Protocol calibrates the Z',
}

#tiprack
if 'tiprack' not in locals():
    tiprack = labware.load('opentrons-tiprack-300ul', '1')
if 'ax_6' not in locals():
    ax_6 = labware.load('ax6_5','3')


if 'pipette_300' not in locals():
    pipette_300 = instruments.P300_Single(mount='left', tip_racks=[tiprack])

piwells =["A1","B1","C1","D1","E1","F1","G1","H1","A2","B2","C2","D2","E2","F2","G2","H2","A3","B3","C3","D3","E3","F3","G3","H3","A4","B4","C4","D4","E4","F4","G4","H4","A5","B5","C5","D5","E5","F5","G5","H5","A6","B6","C6","D6","E6","F6","G6","H6","A7","B7","C7","D7","E7","F7","G7","H7","A8","B8","C8","D8","E8","F8","G8","H8","A9","B9","C9","D9","E9","F9","G9","H9","A10","B10","C10","D10","E10","F10","G10","H10","A11","B11","C11","D11","E11","F11","G11","H11","A12","B12","C12","D12","E12","F12","G12","H12"]
def usetip(val = 3,rst = 1):
    piwells =["A1","B1","C1","D1","E1","F1","G1","H1","A2","B2","C2","D2","E2","F2","G2","H2","A3","B3","C3","D3","E3","F3","G3","H3","A4","B4","C4","D4","E4","F4","G4","H4","A5","B5","C5","D5","E5","F5","G5","H5","A6","B6","C6","D6","E6","F6","G6","H6","A7","B7","C7","D7","E7","F7","G7","H7","A8","B8","C8","D8","E8","F8","G8","H8","A9","B9","C9","D9","E9","F9","G9","H9","A10","B10","C10","D10","E10","F10","G10","H10","A11","B11","C11","D11","E11","F11","G11","H11","A12","B12","C12","D12","E12","F12","G12","H12"]
    if val == 0:
        varwells = rst;
        c_well = varwells;
    elif val == 1:
        with open('tipw', 'rb') as f:
            varwells = pickle.load(f);
            c_well = varwells;
        return piwells[c_well]
    elif val == 2: #return tip fumnction on
        with open('tipw','rb') as f:
            varwells = pickle.load(f)
        if varwells == 0:
            c_well = varwells
        else:
            c_well = varwells-1
    else:
        with open('tipw', 'rb') as f:
            varwells = pickle.load(f);
        c_well = varwells;
        varwells = varwells+1;
    with open('tipw', 'wb') as f:
        pickle.dump(varwells, f);    
    return c_well

def update_offset(labware_name, rd, x_val=0.0, y_val=0.0, z_val=0.0):
    path = '/data/packages/usr/local/lib/python3.6/site-packages/opentrons/shared_data/labware/definitions/2/'
    with open (path + labware_name + '/1.json', 'rt') as myfilelines:
        for i, l in enumerate(myfilelines):
            fileline = i+1;
        myfilelines.close()
    with open (path + labware_name + '/1.json', 'rt') as myfile:  # Open lorem.txt for reading text
        contents = myfile.read()
        for i, l in enumerate(myfile):
            fileline = i;
        if fileline > 1:
            text = re.search('"cornerOffsetFromSlot": {((.*\n){4})', contents)
            index_str = re.search('"cornerOffsetFromSlot": {((.*\n){4})', contents).start()
            index_str_end = re.search('"cornerOffsetFromSlot": {((.*\n){4})', contents).end()
            offset_txt = text.group(1).splitlines()
            offset_txt = list(filter(None, offset_txt))
            offset = [];
            for line in range (len(offset_txt)):
                offset.append(float(re.findall("-?\d+\.\d+", offset_txt[line])[0]))
            string = '\"cornerOffsetFromSlot\": {\n\t\"x\": ' + str(x_val)+',\n\t\"y\": ' + str(y_val)+',\n\t\"z\": ' + str(z_val)

        else:
            text = re.search('"cornerOffsetFromSlot":{(.+?)}', contents)
            index_str = re.search('"cornerOffsetFromSlot":{(.+?)}', contents).start()
            index_str_end = re.search('"cornerOffsetFromSlot":{(.+?)}', contents).end()
            offset_txt = text[1].split(',')
            offset = [];
            for line in range (len(offset_txt)):
                offset.append(float(re.search(':(.+?.*)', offset_txt[line])[1]))
            string = '\"cornerOffsetFromSlot\":{\"x\":' + str(x_val)+',\"y\":' + str(y_val)+',\"z\":' + str(z_val)
                
        if rd == True:       
            myfile.close()
            return offset

        elif rd == False:
            new_json="".join((contents[:index_str], string ,contents[index_str_end-1:]))
            with open(path + labware_name + '/1.json', 'w') as myfile:
                myfile.write(new_json)
                myfile.close()


#try:
#    offset = pickle.load(open("ax_offset.pickle", "rb"))
#except (OSError, IOError) as e:
#    offset = [0,0,0]
#    pickle.dump(offset, open("ax_offset.pickle", "wb"))

offset = update_offset("ax6_5",True)
print(offset)


robot.connect()
robot.home()

#Protocol
pipette_300.pick_up_tip(tiprack.wells(piwells[int(usetip())]))
print(usetip(1))
pipette_300.move_to(ax_6.wells("A1").top(offset[2]))
while True:
    mov_dir = input("Select movement direction and step size: ")
    mov_dir = mov_dir.split()
    calibrate_end = bool(int(mov_dir[2]))
    if calibrate_end == True:
        break
    else:
        step_dir = str(mov_dir[0])
        step_size = float(mov_dir[1])
        calibration_functions.jog_instrument(instrument=pipette_300,distance=step_size,axis=step_dir,robot=robot)
        if step_dir == "x":
            offset[0] = offset[0] + step_size
        elif step_dir == "y":
            offset[1] = offset[1] + step_size
        elif step_dir == "z":
            offset[2] = offset[2] + step_size
            
print(offset)
update_offset("ax6_5",False, offset[0], offset[1], offset[2])

#with open('ax_offset.pickle', 'wb') as f:
#    pickle.dump(offset, f);

pipette_300.return_tip()
usetip(2)
print(usetip(1))
print("Finishing")
robot.home()