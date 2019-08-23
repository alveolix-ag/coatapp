from opentrons import robot
import argparse


parser = argparse.ArgumentParser(description='Process some integers.')
parser.add_argument('-o', '--on', type=str)
args = parser.parse_args() #this is the variable that stores the inputs from the UI
state = str(args.on)

if state == "ON":
	robot._driver.turn_on_rail_lights()
	print("ON")
	
elif state == "OFF":
	robot._driver.turn_off_rail_lights()
	print("OFF")