# coding: utf-8

# In[ ]:

# Echo server program
import socket 
import sys
import argparse

parser = argparse.ArgumentParser(description='Process some integers.')
parser.add_argument('timeout', metavar='N', type=str)
args = parser.parse_args() #this is the variable that stores the inputs from the UI
timeout = int(args.timeout)

HOST = ''                 # Symbolic name meaning all available interfaces
PORT = 50007             # Arbitrary non-privileged port
with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    s.bind((HOST, PORT))
    s.listen(1)
    s.settimeout(timeout)
    conn, addr = s.accept()
    with conn:
        #print('Connected by', addr)
        while True:
            data = conn.recv(1024)
            data1 = conn.send(b'confirm')
            print(data.decode("utf-8"))
            if not data: 
               break
            conn.sendall(data)

#print ("hello")        
conn.close();





