
# coding: utf-8

# In[1]:


import pickle
import socket
import json

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

cu_tip = usetip(1)

with open('host_ip', 'rb') as f:
    cu_ip = pickle.load(f);

HOST = cu_ip    # The remote host
PORT = 11000   # The same port as used by the server


# Create a TCP/IP socket
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

# Connect the socket to the port where the server is listening
server_address = (HOST, PORT)
print('connecting to {} port {}'.format(*server_address))
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