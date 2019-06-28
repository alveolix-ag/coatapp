
# coding: utf-8

# In[ ]:


from opentrons import labware, instruments, robot, containers #Libraries from Opentron API
import signal
import sys 
import pickle #This is used for storing the variable of the curent tiprack well
import subprocess #Allows to run subprocess, video recording
import argparse


# metadata
#Information about 
metadata = {
    'protocolName': 'Chip_coating_rotator',
    'author': 'Daniel Nakhaee-Zadeh Gutierrez',
    'description': 'Protocol used to coat the PDMS membrane of the AX6 LOC chip',
}

#Parser to store the input values from the UI
parser = argparse.ArgumentParser(description='Process some integers.')
parser.add_argument('integers', metavar='N', type=int, nargs='+',
                   help='an integer for the accumulator')

args = parser.parse_args()
print(args.integers)


#Connect to Robot
robot.connect()
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
rotate_st = None;

# labware
if 'ax_6' not in locals():
    ax_6 = labware.load('ax6_4','6')
if 'tiprack' not in locals():
    tiprack = labware.load('opentrons-tiprack-300ul', '1')
if 'ep_rack' not in locals():
    ep_rack = labware.load('opentrons_24_tuberack_eppendorf_2ml_safelock_snapcap','4')


# pipettes
if 'pipette_300' not in locals():
    pipette_300 = instruments.P300_Single(mount='left', tip_racks=[tiprack])
if 'pipette_50' not in locals():
    pipette_50 = instruments.P50_Single(mount='right', tip_racks=[tiprack])
    
#Keeping sanity on tips
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

#Check Current Pipette Tip
print("Ensure that there is pipette tips from well", piwells[int(usetip(1))])
print("If not reset pipette count using usetip(val,rst) function")

#protocol
while 1:
    #side_to_coat = int(input("Coating Apical only (Enter 1), or Coating Apical and Basal side (Enter 2)"))
    side_to_coat = int(args.integers[2])
    if side_to_coat != 0 and side_to_coat != 1 and side_to_coat != 2:
        print('Error - Please enter a correct value (1 or 2)')
    else:
        break

initial_volume = 12*6*num_chips
wells = ["A1","B1","C1","D1","E1","F1","A2","B2","C2","D2","E2","F2","A3","B3","C3","D3","E3","F3"]


#protocol
pipette_300.pick_up_tip(tiprack.wells(piwells[int(usetip())]))
pipette_300.mix(5, initial_volume, ep_rack.wells('A1').bottom(3))
pipette_300.aspirate(initial_volume, ep_rack.wells('A1').bottom(3))
for i in range(0,(6*num_chips)):
    pipette_300.dispense(12, ax_6.wells(wells[i]).top(-1))
    pipette_300.delay(seconds=3) 
  
    
if side_to_coat == 1 or side_to_coat == 0:
    print('Finishing run')
    pipette_300.drop_tip()
    robot.home()
    
elif side_to_coat == 2:
    pipette_300.move_to(ep_rack.wells('A1').top(20))
    print("Flip Chip Holder, rotating around its shorter side")
    while rotate_st is None:
        rotate_st = input('Press enter when chip is flipped...')
    pipette_300.mix(5, initial_volume, ep_rack.wells('A1').bottom(3))
    pipette_300.aspirate(initial_volume, ep_rack.wells('A1').bottom(3))
    for i in range(0 , (6*num_chips)):
        pipette_300.dispense(12, ax_6.wells(wells[i]).top(-0.8))
        pipette_300.delay(seconds=3)
 
    print('Finishing run')
    pipette_300.drop_tip()
    robot.home()
    

