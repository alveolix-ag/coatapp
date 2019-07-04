from opentrons import labware, instruments, robot, containers #Libraries from Opentron API
import signal
import sys 
import pickle #This is used for storing the variable of the curent tiprack well
import subprocess #Allows to run subprocess, video recording
import argparse


# metadata
#Information about the protocol 
metadata = {
    'protocolName': 'Calibrate',
    'author': 'Daniel Nakhaee-Zadeh Gutierrez',
    'description': 'Protocol calibrates the Z-',
}

#tiprack
if 'tiprack' not in locals():
    tiprack = labware.load('opentrons-tiprack-300ul', '1')

if 'pipette_300' not in locals():
	pipette_300 = instruments.P300_Single(mount='left', tip_racks=[tiprack])
#ax-6 definition
if 'ax_6' not in locals():
    ax_6 = labware.load('ax6_4','6')


piwells =["A1","B1","C1","D1","E1","F1","G1","H1","A2","B2","C2","D2","E2","F2","G2","H2","A3","B3","C3","D3","E3","F3","G3","H3","A4","B4","C4","D4","E4","F4","G4","H4","A5","B5","C5","D5","E5","F5","G5","H5","A6","B6","C6","D6","E6","F6","G6","H6","A7","B7","C7","D7","E7","F7","G7","H7","A8","B8","C8","D8","E8","F8","G8","H8","A9","B9","C9","D9","E9","F9","G9","H9","A10","B10","C10","D10","E10","F10","G10","H10","A11","B11","C11","D11","E11","F11","G11","H11","A12","B12","C12","D12","E12","F12","G12","H12"]
def usetip(val = 3,rst = 0):
    if val == 0:
        varwells = rst;
    elif val == 1:
        with open('tipwells', 'rb') as f:
            varwells = pickle.load(f);
    else:
        with open('tipwells', 'rb') as f:
            varwells = pickle.load(f);
        varwells = varwells+1;
    with open('tipwells', 'wb') as f:
        pickle.dump(varwells, f);
    return varwells


with open('z_calibration', 'rb') as f:
	z_initial = pickle.load(f);


robot.connect()
robot.home()

#Protocol
pipette_300.pick_up_tip(tiprack.wells(piwells[int(usetip())]))
pipette_300.move_to(ax_6.wells("A1").top(z_initial))
while True:
    z_dir = float(input("Up or Down"))
	if z_dir == 0:
		break
	else:
	   pipette_300.move_to(ax_6.wells("A1").top(z_initial+z_dir))
	   z_initial = z_initial + z_dir



with open('z_calibration', 'wb') as f:
	pickle.dump(z_initial, f);

pipette_300.return_tip()
robot.home()
