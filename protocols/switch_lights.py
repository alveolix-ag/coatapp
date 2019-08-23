from opentrons import robot
import argparse


parser = argparse.ArgumentParser(description='Process some integers.')
parser.add_argument('-o', '--on', type=bool)
args = parser.parse_args() #this is the variable that stores the inputs from the UI
state = args.on

if state == True:
	robot._driver.turn_on_rail_lights()
	
elif state == False:
	robot._driver.turn_off_rail_lights()