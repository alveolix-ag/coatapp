#create_setup.py
from opentrons import labware, instruments, robot, containers #Libraries from Opentron API
import signal
import sys 
import pickle #This is used for storing the variable of the curent tiprack well
import subprocess #Allows to run subprocess, video recording
import argparse
import socket

#path = '/data/coatapp_config/setup'
path = '/data/coatapp_config/setup'
file = open(path, "r")
lines = file.readlines()
labwareline = []
i=0;
for line in lines:
	labwareline[i] = line.split(',')
	i += 1





