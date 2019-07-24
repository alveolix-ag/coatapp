
# coding: utf-8

# In[1]:


from opentrons import labware, instruments, robot, containers
import signal
import sys
import math
import pickle
import argparse
from operator import add
from functools import reduce


# metadata
metadata = {
    'protocolName': 'Chip_Wash_Rotator',
    'author': 'Daniel Nakhaee-Zadeh Gutierrez',
    'description': 'Protocol used to wash the chip coatings from the AX6 chip',
}


parser = argparse.ArgumentParser(description='Process some integers.')
parser.add_argument('integers', metavar='N', type=int, nargs='+',
                   help='an integer for the accumulator')
parser.add_argument('-w', '--well', nargs='+', default=[])
parser.add_argument('-v', '--volume', metavar='N', type=int, nargs='+')
parser.add_argument('-z', '--z_space', metavar='N', type=int, nargs='+')
parser.add_argument('-f', '--flow_rate', metavar='N', type=int, nargs='+')
parser.add_argument('-na', '--num_aspiration', metavar='N', type=int, nargs='+')

args = parser.parse_args()
print(args.integers)
# args.integers[1] = number of chips to coat
# args.integers[0] = speed configuration
# args.integers[2] = side to coat
# args.integers[3] = number of washes
# args.integers[4] = advance mode

#Connect to Robot
robot.connect()
robot.home() #home robot axes

#set motor speed
slow_speed = {'x': 100, 'y': 100, 'z': 20, 'a': 20, 'b': 10, 'c': 10}
fast_speed = {'x': 500, 'y': 300, 'z': 100, 'a': 100, 'b': 40, 'c': 40}
medium_speed = {'x': 300, 'y': 200, 'z': 75, 'a': 75, 'b': 25, 'c': 25}
speed_conf = int(args.integers[0])
#speed_conf = int(input('set robot speed to fast (Enter 1), medium (Enter 2) or slow (Enter 3)'))
if speed_conf ==0:
    speed_set = fast_speed
elif speed_conf ==1:
    speed_set = medium_speed
elif speed_conf ==2:
    speed_set = slow_speed
    
robot.head_speed(combined_speed=max(speed_set.values()),**speed_set)

#num of chips
num_chips = int(args.integers[1])
num_wash = int(args.integers[3])
print("number of wash: ", num_wash)
rotate_st = None;#
with open('z_calibration', 'rb') as f:
    z_distance = pickle.load(f);

#num_chips = int(input('Number of chips to coat (1,2 or 3)'))


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
    

wells = ["A1","B1","C1","D1","E1","F1","A2","B2","C2","D2","E2","F2","A3","B3","C3","D3","E3","F3"]

#Keeping sanity on tips
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

# Side to coat
while True:
    #side_to_coat = int(input("Coating Apical only (Enter 1), or Coating Apical and Basal side (Enter 2)"))
    side_to_coat = int(args.integers[2])
    if side_to_coat != 2 and side_to_coat != 1 and side_to_coat != 0:
        print('Error - Please enter a correct value (1 or 2)')
    else:
        break

#Check Current Pipette Tip
#print("Ensure that there is pipette tips from well", piwells[int(usetip(1))])
#print("If not reset pipette count using usetip(val,rst) function")

### protocol

advance_mode = args.integers[4]
# Advance protocol: control over individual wells
if advance_mode == 1:
    wells_to_coat = args.well;
    volume_list = [float(i) for i in args.volume];
    z_list = [float(i) for i in args.z_space];
    flow_rate = [float(i) for i in args.flow_rate];
    num_asp = [float(i) for i in args.num_aspiration];
    vol_in = 0;
    vol_mat = reduce(add ,[[a]*b for a, b in zip(volume_list, num_asp)])
    d=0;

    #removing remaining coating
    pipette_300.pick_up_tip(tiprack.wells(piwells[int(usetip())]))
    for i in range(len(wells_to_coat)):
        for u in range(num_asp[i]):
            pipette_300.aspirate(volume_list[i], ax_6.wells(wells_to_coat[i]).top(z_distance+z_list[i]))
            pipette_300.delay(seconds=3)
            vol_in = vol_mat[d] + vol_in;
            if d == len(vol_mat)-1:
                pipette_300.dispense(ep_rack.wells('A3').top(-1)) 
                pipette_300.blow_out()
                pipette_300.touch_tip(-2) 
                vol_in = 0
            elif vol_in + vol_mat[d+1] >= 300:
                pipette_300.dispense(ep_rack.wells('A3').top(-1)) 
                pipette_300.blow_out()
                pipette_300.touch_tip(-2)
                vol_in = 0;
            d = d+1    


#aspirating
pipette_300.set_flow_rate(aspirate=400)
pipette_300.pick_up_tip(tiprack.wells(piwells[int(usetip())]))
for u in range(3):
    for i in range(6*num_chips):
        pipette_300.aspirate(20, ax_6.wells(wells[i]).top(-1.6))
        if i == 11:
            pipette_300.dispense(ep_rack.wells('A3').top(-1)) 
            pipette_300.blow_out()
            pipette_300.touch_tip(-2)     
    pipette_300.dispense(ep_rack.wells('A3').top(-1))
    pipette_300.blow_out()
    pipette_300.touch_tip(-2)      

pipette_300.drop_tip()

#washing
washing_volume = 25
initial_volume = washing_volume*6*num_chips
wells_cover = int(300/(initial_volume/(6*num_chips)))
pipette_300.pick_up_tip(tiprack.wells(piwells[int(usetip())]))

for h in range(num_wash):
    d = 0;
    p = 0;
    r = (int(wells_cover));

    if initial_volume > 300:
        while d < math.ceil(6*num_chips/wells_cover):
            pipette_300.aspirate(300, ep_rack.wells('A2').bottom(3))
            for i in range(p,r):
                pipette_300.dispense(25, ax_6.wells(wells[i]).top())
                pipette_300.mix(5, 20, ax_6.wells(wells[i]).top(-1))
            pipette_300.dispense(ep_rack.wells('A3').top(-1))
            pipette_300.blow_out()
            pipette_300.touch_tip(-2)     
            p=i+1;
            d=d+1
            if wells_cover*(d+1) >= 6*num_chips:
                r= num_chips*6
            else:
                r=int(wells_cover)*d
   
    else: 
        pipette_300.aspirate(initial_volume, ep_rack.wells('A2').bottom(3))
        for i in range(num_chips*6):
            pipette_300.dispense(25, ax_6.wells(wells[i]).top(-0.5))
            pipette_300.mix(5, 20, ax_6.wells(wells[i]).top(-1))

#Remove washing liquid and dry
    for t in range(3):
        for i in range(num_chips*6):
            if (washing_volume*i) == 300:
                pipette_300.dispense(ep_rack.wells('A3').top(-1))
                pipette_300.blow_out()
                pipette_300.touch_tip(-2) 
            pipette_300.aspirate(washing_volume, ax_6.wells(wells[i]).top(-1.6))
        pipette_300.dispense(ep_rack.wells('A3').top(-1))
        pipette_300.blow_out()
        pipette_300.touch_tip(-2)      
    
pipette_300.drop_tip()

if side_to_coat == 2:
    print("Flip Chip Holder")
    while rotate_st is None:
        rotate_st = input('Press enter when chip is flipped...')
    #aspirating
    pipette_300.set_flow_rate(aspirate=400)
    pipette_300.pick_up_tip(tiprack.wells(piwells[int(usetip())]))
    for u in range(3):
        for i in range(6*num_chips):
            pipette_300.aspirate(20, ax_6.wells(wells[i]).top(-1.1))
            if i == 11:
                pipette_300.dispense(ep_rack.wells('A3').top(-1))
                pipette_300.blow_out()
                pipette_300.touch_tip(-2)     
        pipette_300.dispense(ep_rack.wells('A3').top(-1))
        pipette_300.blow_out()
        pipette_300.touch_tip(-2)     

    pipette_300.drop_tip()

    #washing
    washing_volume = 25
    initial_volume = washing_volume*6*num_chips
    wells_cover = int(300/(initial_volume/(6*num_chips)))
    pipette_300.pick_up_tip(tiprack.wells(piwells[int(usetip())]))

    for h in range(num_wash):
        d = 0;
        p = 0;
        r = (int(wells_cover));

        if initial_volume > 300:
            while d < math.ceil(6*num_chips/wells_cover):
                pipette_300.aspirate(300, ep_rack.wells('A2').bottom(3))
                for i in range(p,r):
                    pipette_300.dispense(25, ax_6.wells(wells[i]).top())
                    pipette_300.mix(5, 20, ax_6.wells(wells[i]).top(-1))
                pipette_300.dispense(ep_rack.wells('A3').top(-1))
                pipette_300.blow_out()
                pipette_300.touch_tip(-2)      
                p=i+1;
                d=d+1
                if wells_cover*(d+1) >= 6*num_chips:
                    r= num_chips*6
                else:
                    r=int(wells_cover)*d
   
        else: 
            pipette_300.aspirate(initial_volume, ep_rack.wells('A2').bottom(3))
            for i in range(num_chips*6):
                pipette_300.dispense(25, ax_6.wells(wells[i]).top())
                pipette_300.mix(5, 20, ax_6.wells(wells[i]).top(-0.5))

    #Remove washing liquid and dry
        for t in range(3):
            for i in range(num_chips*6):
                if (washing_volume*i) == 300:
                    pipette_300.dispense(ep_rack.wells('A3').top(-1))
                    pipette_300.blow_out()
                    pipette_300.touch_tip(-2)      
                pipette_300.aspirate(washing_volume, ax_6.wells(wells[i]).top(-1.1))
            pipette_300.dispense(ep_rack.wells('A3').top(-1))
            pipette_300.blow_out()
            pipette_300.touch_tip(-2)      
    
    pipette_300.drop_tip()


print('finishing run')
robot.home()

