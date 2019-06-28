
# coding: utf-8

# In[ ]:


from opentrons import labware, instruments, robot, containers
import signal
import sys
import pickle
import subprocess

robot.connect()
print("Homing Robot")
robot.home()

