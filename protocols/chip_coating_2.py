
# coding: utf-8

# In[1]:


from opentrons import labware, instruments, robot, containers
import signal
import sys

# metadata
metadata = {
    'protocolName': 'Chip_coating_2',
    'author': 'Daniel Nakhaee-Zadeh Gutierrez',
    'description': 'Protocol used to coat the PDMS membrane of the AX6 LOC chip',
}

#Connect to Robot
robot.connect()
robot.home() #home robot axes

#set motor speed
slow_speed = {'x': 100, 'y': 100, 'z': 20, 'a': 20, 'b': 10, 'c': 10}
fast_speed = {'x': 500, 'y': 300, 'z': 100, 'a': 100, 'b': 40, 'c': 40}
medium_speed = {'x': 300, 'y': 200, 'z': 75, 'a': 75, 'b': 25, 'c': 25}
speed_conf = int(input('set robot speed to fast (Enter 1), medium (Enter 2) or slow (Enter 3)'))
if speed_conf ==1:
    speed_set = fast_speed
elif speed_conf ==2:
    speed_set = medium_speed
elif speed_conf ==3:
    speed_set = slow_speed
    
robot.head_speed(combined_speed=max(speed_set.values()),**speed_set)


# labware
ax_6 = labware.load('ax61','3')
tiprack = labware.load('opentrons-tiprack-300ul', '1')
tuberack = labware.load('opentrons-tuberack-50ml', '6')
ep_rack = labware.load('opentrons-tuberack-2ml-eppendorf','5')


# pipettes
pipette_300 = instruments.P300_Single(mount='left', tip_racks=[tiprack])
pipette_50 = instruments.P50_Single(mount='right', tip_racks=[tiprack])

#Abort protocol
#def signal_handler(signal, frame):
#    print('User Interrupt')
#    robot.home()
#    pipette_300.drop_tip()
#    robot.home()
#    sys.exit(0)
#signal.signal(signal.SIGINT, signal_handler)

#protocol
while 1:
    side_to_coat = int(input("Coating Apical only (Enter 1), or Coating Apical and Basal side (Enter 2)"))
    if side_to_coat != 2 and side_to_coat != 1:
        print('Error - Please enter a correct value (1 or 2)')
    else:
        break

initial_volume = 12*12        

pipette_300.pick_up_tip(tiprack.wells('A1'))
pipette_300.aspirate(initial_volume, ep_rack.wells('A1').bottom(3))
pipette_300.dispense(12, ax_6.wells('A1').top())
pipette_300.dispense(12, ax_6.wells('B1').top())
pipette_300.dispense(12, ax_6.wells('C1').top())
pipette_300.dispense(12, ax_6.wells('D1').top())
pipette_300.dispense(12, ax_6.wells('E1').top())
pipette_300.dispense(12, ax_6.wells('F1').top())
pipette_300.dispense(12, ax_6.wells('A2').top())
pipette_300.dispense(12, ax_6.wells('B2').top())
pipette_300.dispense(12, ax_6.wells('C2').top())
pipette_300.dispense(12, ax_6.wells('D2').top())
pipette_300.dispense(12, ax_6.wells('E2').top())
pipette_300.dispense(12, ax_6.wells('F2').top())
    
if side_to_coat == 1:
    print('Finishing run')
    pipette_300.drop_tip()
    robot.home()
elif side_to_coat == 2:
    pipette_300.move_to(ep_rack.wells('A1').top(20))
    print("Flip Chip Holder, rotating around its shorter side")
    input('Press enter when chip is flipped...')
    pipette_300.aspirate(initial_volume, ep_rack.wells('A1').bottom(3))
    pipette_300.dispense(12, ax_6.wells('A1').top())
    pipette_300.dispense(12, ax_6.wells('B1').top())
    pipette_300.dispense(12, ax_6.wells('C1').top())
    pipette_300.dispense(12, ax_6.wells('D1').top())
    pipette_300.dispense(12, ax_6.wells('E1').top())
    pipette_300.dispense(12, ax_6.wells('F1').top())
    pipette_300.dispense(12, ax_6.wells('A2').top())
    pipette_300.dispense(12, ax_6.wells('B2').top())
    pipette_300.dispense(12, ax_6.wells('C2').top())
    pipette_300.dispense(12, ax_6.wells('D2').top())
    pipette_300.dispense(12, ax_6.wells('E2').top())
    pipette_300.dispense(12, ax_6.wells('F2').top())
    print('Finishing run')
    pipette_300.drop_tip()
    robot.home()
        

