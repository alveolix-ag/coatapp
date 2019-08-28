import pickle
import socket

import socket 
import sys
import argparse


HOST = ''                 # Symbolic name meaning all available interfaces
PORT = 50007             # Arbitrary non-privileged port
with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    s.bind((HOST, PORT))
    s.listen(1)
    #s.settimeout(timeout)
    conn, addr = s.accept()
    with conn:
        #print('Connected by', addr)
        while True:
            data = conn.recv(1024)
            if data == "Tip":
            	print(data.decode("utf-8"))
            	data1 = conn.send(b'Confirm Tip')
            if data == "finished": 
               break
            #conn.sendall(data)

#print ("hello")        
conn.close();