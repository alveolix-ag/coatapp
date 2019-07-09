
# coding: utf-8

# In[ ]:


from opentrons import labware, instruments, robot, containers

robot.connect()
print("Homing Robot")
robot.home()

