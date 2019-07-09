# Echo client program
import socket

HOST = '169.254.87.247'    # The remote host
PORT = 50007            # The same port as used by the server
with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
	s.connect((HOST, PORT))
	rotate_state = input("rotate state(1)apical or basal(2)")
	rotate_encode = str.encode(rotate_state)
	s.send(rotate_encode)
	data = s.recv(1024)
print('Received', repr(data))
