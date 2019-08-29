#Initial Coating Protocol
#Something to Test 12345 6shshshsh

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

with open('host_ip', 'rb') as f:
    cu_ip = pickle.load(f);

HOST = "169.254.11.184"   # The remote host
PORT = 5018   # The same port as used by the server

# Create a TCP/IP socket

# Connect the socket to the port where the server is listening
server_address = (HOST, PORT)


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
    elif val == 2: #return tip function on
        with open('tipw','rb') as f:
            varwells = pickle.load(f)
        if varwells == 0:
            varwells = 0
        else:
            varwells = varwells-1
        c_well = varwells
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



    #side_to_coat = int(input("Coating Apical only (Enter 1), or Coating Apical and Basal side (Enter 2)"))
side_to_coat = int(args.integers[2])

#Define intial volume
initial_volume = 13*6*num_chips
wells = ["A1","B1","C1","D1","E1","F1","A2","B2","C2","D2","E2","F2","A3","B3","C3","D3","E3","F3"]

print("Coating")
#Standard Protocol
pipette_300.pick_up_tip(tiprack.wells(piwells[int(usetip())]))
pipette_300.mix(5, 300, ep_rack.wells('A1').bottom(3))
pipette_300.aspirate(initial_volume+4, ep_rack.wells('A1').bottom(3))
for i in range(0,(6*num_chips)):
    pipette_300.dispense(12, ax_6.wells(wells[i]).top())
    pipette_300.delay(seconds=3)

if side_to_coat == 2:
    pipette_300.move_to(ep_rack.wells('A1').top(20))
    print("Flip Chip Holder, rotating around its shorter side")
    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
        s.connect(server_address)
    #This connects to the OT-APP and send a command to rotate the chip holder rotator  
        try:
    # Send data
            message = "rotate: 2"
            s.sendall(message.encode())
            data = s.recv(1024)
            print('received {!r}'.format(data.decode("utf-8")))
    
        finally:
            print("OK")
            s.close()

    pipette_300.delay(seconds = 3)     
    pipette_300.mix(5, initial_volume, ep_rack.wells('A1').bottom(3))
    pipette_300.aspirate(initial_volume, ep_rack.wells('A1').bottom(3))
    for i in range(0 , (6*num_chips)):
        pipette_300.dispense(12, ax_6.wells(wells[i]).top())
        pipette_300.delay(seconds=3)

print('Finishing run')
pipette_300.drop_tip()

robot.home()

#This code is used to update the coatapp about the end of the protocol
sock.connect(server_address)
try:
    # Send data
    message = {"tip": cu_tip}
    data_string = json.dumps(message)
    print('sending {!r}'.format(data_string))
    sock.sendall(data_string.encode())
    data = sock.recv(1024)
    print('received {!r}'.format(data.decode("utf-8")))
    
finally:
    print("OK")

try:
    # Send data
    message = "finish"
    print('sending {!r}'.format(message))
    sock.sendall(message.encode())
    data = sock.recv(1024)
    print('received {!r}'.format(data.decode("utf-8")))
    
finally:
    print('closing socket')
    sock.close()
