
# coding: utf-8

# In[1]:


from opentrons import labware, instruments, robot, containers

# metadata
metadata = {
    'protocolName': 'Chip_coating_1',
    'author': 'Daniel Nakhaee-Zadeh Gutierrez',
    'description': 'Protocol used to coat the PDMS membrane of the AX6 LOC chip',
}

#setup config

#set motor speed
slow_speed = {'x': 200, 'y': 200, 'z': 50, 'a': 50, 'b': 20, 'c': 20}
robot.head_speed(combined_speed=max(slow_speed.values()),**slow_speed)

#Connect to Robot
robot.connect()
robot.home() #home robot axes

# labware
plate = labware.load('ax61','3')
tiprack = labware.load('opentrons-tiprack-300ul', '1')
tuberack = labware.load('opentrons-tuberack-50ml', '6')
ep_rack = labware.load('opentrons-tuberack-2ml-eppendorf','5')


# pipettes
pipette_300 = instruments.P300_Single(mount='left', tip_racks=[tiprack])
pipette_50 = instruments.P50_Single(mount='right', tip_racks=[tiprack])

#protocol
while 1:
    side_to_coat = int(input("Coating Apical only (Enter 1), or Coating Apical and Basal side (Enter 2)"))
    if side_to_coat != 2 and side_to_coat != 1:
        print('Error - Please enter a correct value (1 or 2)')
    else:
        break
    
pipette_50.pick_up_tip(tiprack.wells('A1'))
pipette_50.transfer(12, ep_rack.wells('A1').bottom(2), plate.wells('A1').top(),new_tip='never')
pipette_50.transfer(12, ep_rack.wells('A1').bottom(2), plate.wells('B1').top(),new_tip='never')
pipette_50.transfer(12, ep_rack.wells('A1').bottom(2), plate.wells('C1').top(),new_tip='never')
pipette_50.transfer(12, ep_rack.wells('A1').bottom(2), plate.wells('D1').top(),new_tip='never')
pipette_50.transfer(12, ep_rack.wells('A1').bottom(2), plate.wells('E1').top(),new_tip='never')
pipette_50.transfer(12, ep_rack.wells('A1').bottom(2), plate.wells('F1').top(),new_tip='never')
pipette_50.transfer(12, ep_rack.wells('A1').bottom(2), plate.wells('A2').top(),new_tip='never')
pipette_50.transfer(12, ep_rack.wells('A1').bottom(2), plate.wells('B2').top(),new_tip='never')
pipette_50.transfer(12, ep_rack.wells('A1').bottom(2), plate.wells('C2').top(),new_tip='never')
pipette_50.transfer(12, ep_rack.wells('A1').bottom(2), plate.wells('D2').top(),new_tip='never')
pipette_50.transfer(12, ep_rack.wells('A1').bottom(2), plate.wells('E2').top(),new_tip='never')
pipette_50.transfer(12, ep_rack.wells('A1').bottom(2), plate.wells('F2').top(),new_tip='never')
    
if side_to_coat == 1:
    print('Finishing run')
    pipette_50.drop_tip()
    robot.home()
elif side_to_coat == 2:
    pipette_50.move_to(ep_rack.wells('A1').top(20))
    print("Flip Chip Holder, rotating around its shorter side")
    input('Press enter when chip is flipped...')
    pipette_50.transfer(12, ep_rack.wells('A1').bottom(2), plate.wells('A1').top(),new_tip='never')
    pipette_50.transfer(12, ep_rack.wells('A1').bottom(2), plate.wells('B1').top(),new_tip='never')
    pipette_50.transfer(12, ep_rack.wells('A1').bottom(2), plate.wells('C1').top(),new_tip='never')
    pipette_50.transfer(12, ep_rack.wells('A1').bottom(2), plate.wells('D1').top(),new_tip='never')
    pipette_50.transfer(12, ep_rack.wells('A1').bottom(2), plate.wells('E1').top(),new_tip='never')
    pipette_50.transfer(12, ep_rack.wells('A1').bottom(2), plate.wells('F1').top(),new_tip='never')
    pipette_50.transfer(12, ep_rack.wells('A1').bottom(2), plate.wells('A2').top(),new_tip='never')
    pipette_50.transfer(12, ep_rack.wells('A1').bottom(2), plate.wells('B2').top(),new_tip='never')
    pipette_50.transfer(12, ep_rack.wells('A1').bottom(2), plate.wells('C2').top(),new_tip='never')
    pipette_50.transfer(12, ep_rack.wells('A1').bottom(2), plate.wells('D2').top(),new_tip='never')
    pipette_50.transfer(12, ep_rack.wells('A1').bottom(2), plate.wells('E2').top(),new_tip='never')
    pipette_50.transfer(12, ep_rack.wells('A1').bottom(2), plate.wells('F2').top(),new_tip='never')
    print('Finishing run')
    pipette_50.drop_tip()
    robot.home()
        

