class chippie_library:
    def __init__(self, well, position, volume):
        self.well = color
        self.position = position
        self.volume = volume


	def aspirate_from_well(self, volume, z_change = 0):
	    positions=[[0.5,"x"],[-0.5,"x"],[0.5,"y"],[-0.5,"y"]];
	    volume=int(volume/5)
	    for it in range (5):
	        if it == 0:
	            pipette_300.aspirate(volume, ax_6.wells(well).top(z_change))
	            pipette_300.delay(seconds=2)
	            speed_set = {'x': 100, 'y': 100, 'z': 20, 'a': 20, 'b': 10, 'c': 10}
	            robot.head_speed(combined_speed=max(speed_set.values()),**speed_set)
	        else:
	            pipette_300.move_to(ax_6.wells(well).top(z_change))
	            calibration_functions.jog_instrument(instrument=pipette_300,distance=positions[it-1][0],axis=positions[it-1][1],robot=robot)
	            pipette_300.aspirate(volume)
	            pipette_300.delay(seconds=1)
	    speed_set = {'x': 300, 'y': 200, 'z': 75, 'a': 75, 'b': 25, 'c': 25}
	    robot.head_speed(combined_speed=max(speed_set.values()),**speed_set)

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
