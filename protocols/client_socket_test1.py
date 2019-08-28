import pickle
import socket

with open('host_ip', 'rb') as f:
    cu_ip = pickle.load(f);

HOST = "169.254.11.184"    # The remote host
PORT = 50005   # The same port as used by the server
with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    s.connect((HOST, PORT))
    rotate_st = str.encode("Tip")
    s.send(rotate_st)
    #Data is received when the chip is correctly flipped.
    data = s.recv(1024)
    print('Received', repr(data))

hello = input("hello")


with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    s.connect((HOST, PORT))
    state = str.encode("Rotate")
    s.send(state)
    data = s.recv(1024)
    print('Received', repr(data))

hello = input("hello")	

with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    s.connect((HOST, PORT))
    state1 = str.encode("finished")
    s.send(state1)
    data = s.recv(1024)
    print('Received', repr(data))

hello = input("hello")