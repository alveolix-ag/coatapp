#Initial Coating Protocol
#Something to Test

##Importing Libraries
from opentrons import labware, instruments, robot, containers #Libraries from Opentron API
import signal
import sys 
import pickle #This is used for storing the variable of the curent tiprack well
import subprocess #Allows to run subprocess, video recording
import argparse
import socket


# metadata
#Information about the protocol 
metadata = {
    'protocolName': 'Chip_coating_rotator',
    'author': 'Daniel Nakhaee-Zadeh Gutierrez',
    'description': 'Protocol used to coat the PDMS membrane of the AX6 LOC chip',
}

#Parser to store the input values from the UI OT-APP
parser = argparse.ArgumentParser(description='Process some integers.')
parser.add_argument('integers', metavar='N', type=int, nargs='+',
                   help='an integer for the accumulator')
parser.add_argument('-w', '--well', nargs='+', default=[])
parser.add_argument('-v', '--volume', metavar='N', type=int, nargs='+')
parser.add_argument('-z', '--z_space', metavar='N', type=float, nargs='+')
#To add a new argument you can use the <parser>.add_argument('-<initial>', '--<argument name>', type = <type of variable>)
#when calling the protcol from the command line you can add your argument by adding the (-<initial>, EX: -v) followed by the value (EX: 25.0). EX: python3 chip_coating.py -v 25.0

args = parser.parse_args() #this is the variable that stores the inputs from the UI
print(args.integers)
# args.integers[1] = number of chips to coat
# args.integers[0] = speed configuration
# args.integers[2] = side to coat
# args.integers[3] = advance well mode



#Connect to Robot
robot.connect() #necesary to connect with OT-2
robot.home() #home robot axes

#set motor speed
slow_speed = {'x': 100, 'y': 100, 'z': 20, 'a': 20, 'b': 10, 'c': 10}
fast_speed = {'x': 500, 'y': 300, 'z': 100, 'a': 100, 'b': 40, 'c': 40}
medium_speed = {'x': 300, 'y': 200, 'z': 75, 'a': 75, 'b': 25, 'c': 25}
#speed_conf = int(input('set robot speed to fast (Enter 1), medium (Enter 2) or slow (Enter 3)'))
speed_conf = int(args.integers[0])
if speed_conf ==0:
    speed_set = fast_speed
elif speed_conf ==1:
    speed_set = medium_speed
elif speed_conf ==2:
    speed_set = slow_speed
robot.head_speed(combined_speed=max(speed_set.values()),**speed_set)

#Number of chips to coat
#num_chips = int(input('Number of chips to coat (1,2 or 3)'))
num_chips = int(args.integers[1])
#this variable when change to something other that None allows the while loop to be broken.
rotate_st = None;
with open('z_calibration', 'rb') as f:
    z_distance = pickle.load(f);

# Import labware
#ax-6 definition
if 'ax_6' not in locals():
    ax_6 = labware.load('ax6_5','6')
#tiprack
if 'tiprack' not in locals():
    tiprack = labware.load('opentrons-tiprack-300ul', '1')
#eppendorf rack
if 'ep_rack' not in locals():
    ep_rack = labware.load('opentrons_24_tuberack_eppendorf_2ml_safelock_snapcap','4')


# Import pipette definitions
if 'pipette_300' not in locals():
    pipette_300 = instruments.P300_Single(mount='left', tip_racks=[tiprack])
if 'pipette_50' not in locals():
    pipette_50 = instruments.P50_Single(mount='right', tip_racks=[tiprack])
    
#This function stores the current tip that the robot will use and can reset the pipette wells or display the current tip.
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
    else:
        with open('tipw', 'rb') as f:
            varwells = pickle.load(f);
        c_well = varwells;
        varwells = varwells+1;
    with open('tipw', 'wb') as f:
        pickle.dump(varwells, f);    
    return c_well

#Check Current Pipette Tip
#print("Ensure that there is pipette tips from well", piwells[int(usetip(1))])
#print("If not reset pipette count using usetip(val,rst) function")


while True:
    #side_to_coat = int(input("Coating Apical only (Enter 1), or Coating Apical and Basal side (Enter 2)"))
    side_to_coat = int(args.integers[2])
    if side_to_coat != 0 and side_to_coat != 1 and side_to_coat != 2:
        print('Error - Please enter a correct value (1 or 2)')
    else:
        break

#Define intial volume
initial_volume = 13*6*num_chips
wells = ["A1","B1","C1","D1","E1","F1","A2","B2","C2","D2","E2","F2","A3","B3","C3","D3","E3","F3"]

print("Coating")

#if side_to_coat == 0 or side_to_coat == 2: #Side to coat = 1 is coating apical, side to coat = 1 will coat basal and side to coat = 2 is apical and basal
#    z_coat = -1.2
#elif side_to_coat ==1:
#    z_coat =-1

#Socket server settings
with open('host_ip', 'rb') as f:
    cu_ip = pickle.load(f);

HOST = cu_ip    # The remote host
PORT = 50004   # The same port as used by the server

### protocol
advance_mode = args.integers[3]

# Advance protocol: control over individual wells
def aspirate_vol(vol_array, in_well = 0): #this function controls the volume and pippette used to aspirate the the input volume
    initial_volume = 0;
    if vol_array[in_well] < 30:
        default_pipette = 50
        limit_vol = 50
    else:
        default_pipette = 300
        limit_vol = 300
    for d in range(in_well,len(vol_array)):
        if initial_volume + vol_array[d] >= limit_vol or (vol_array[d] < 30 and limit_vol == 300):
            lst_well = d
            return initial_volume, lst_well, default_pipette
            break
        else:
            initial_volume = initial_volume + vol_array[d]
            if d == len(vol_array)-1:
                lst_well = len(vol_array)
                return initial_volume, lst_well, default_pipette
                break


if advance_mode == 1:
    wells_to_coat = args.well;
    volume_list = [float(i) for i in args.volume];
    z_list = [float(i) for i in args.z_space];
    if all(v < 50 for v in volume_list):
        pipette_50.pick_up_tip(tiprack.wells(piwells[int(usetip())]))
        pipette_50.mix(5, 50, ep_rack.wells('A1').bottom(3))
        pip_state = 0
    elif all(v > 30 for v in volume_list):
        pipette_300.pick_up_tip(tiprack.wells(piwells[int(usetip())]))
        pipette_300.mix(5, 300, ep_rack.wells('A1').bottom(3))
        pip_state = 1
    else:
        pipette_300.pick_up_tip(tiprack.wells(piwells[int(usetip())]))
        pipette_50.pick_up_tip(tiprack.wells(piwells[int(usetip())]))
        if (volume_list[0] > 30):
            pipette_300.mix(5, 300, ep_rack.wells('A1').bottom(3))
        else:
            pipette_50.mix(5, 50, ep_rack.wells('A1').bottom(3))
        pip_state = 2
    print(wells_to_coat, volume_list, z_list)
    index_well= 0;
    index_last = 0;
    while index_well < len(wells_to_coat):
        [vol, index_well, pipette_de] = aspirate_vol(volume_list, index_well)
        if pipette_de == 300:
            print("aspirating: ", vol, "ul")
            pipette_300.aspirate(vol, ep_rack.wells('A1').bottom(3))
        elif pipette_de ==50:
            print("aspirating: ", vol, "ul")
            pipette_50.aspirate(vol, ep_rack.wells('A1').bottom(3))
        for i in range (index_last, index_well):
            if pipette_de == 300:
                pipette_300.dispense(volume_list[i], ax_6.wells(wells_to_coat[i]).top(z_distance+z_list[i]))
                pipette_300.delay(seconds=3)
            elif pipette_de == 50:
                pipette_50.dispense(volume_list[i], ax_6.wells(wells_to_coat[i]).top(z_distance+z_list[i]))
                pipette_50.delay(seconds=3)
        index_last = index_well;   
        print(index_last) 

    print('Finishing run')
    if pip_state == 0:
        pipette_50.drop_tip()
    elif pip_state == 1:
        pipette_300.drop_tip()
    else:
        pipette_300.drop_tip()
        pipette_50.drop_tip()    

#Standard Protocol
else:
    pipette_300.pick_up_tip(tiprack.wells(piwells[int(usetip())]))
    pipette_300.mix(5, 300, ep_rack.wells('A1').bottom(3))
    pipette_300.aspirate(initial_volume+4, ep_rack.wells('A1').bottom(3))
    for i in range(0,(6*num_chips)):
        pipette_300.dispense(12, ax_6.wells(wells[i]).top(z_distance))
        pipette_300.delay(seconds=3)
    
    if side_to_coat == 2:
        pipette_300.move_to(ep_rack.wells('A1').top(20))
        print("Flip Chip Holder, rotating around its shorter side")
        #This connects to the OT-APP and send a command to rotate the chip holder rotator
        with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
            s.connect((HOST, PORT))
            rotate_st = str.encode("rotate")
            s.send(rotate_st)
            #Data is received when the chip is correctly flipped.
            data = s.recv(1024)
            print('Received', repr(data))
        #Manually continue the protocol once the chip rotator is flipped.
        while rotate_st is None:
            rotate_st = input('Press enter when chip is flipped...')
        pipette_300.delay(seconds = 3)     
        pipette_300.mix(5, initial_volume, ep_rack.wells('A1').bottom(3))
        pipette_300.aspirate(initial_volume, ep_rack.wells('A1').bottom(3))
        for i in range(0 , (6*num_chips)):
            pipette_300.dispense(12, ax_6.wells(wells[i]).top(z_distance))
            pipette_300.delay(seconds=3)
 
    print('Finishing run')
    pipette_300.drop_tip()

robot.home()