import signal
import sys 
import pickle #This is used for storing the variable of the curent tiprack well
import argparse

parser = argparse.ArgumentParser(description='Process the wells to reset.')
parser.add_argument('-ip', '--ip_address', nargs='+', default=[])
args = parser.parse_args();
ip_received = args.ip_address[0];

with open('host_ip', 'wb') as f:
    pickle.dump(ip_received, f);


