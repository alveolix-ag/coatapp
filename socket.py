# Echo server program
import socket

HOST = ''                 # Symbolic name meaning all available interfaces
PORT = 50006             # Arbitrary non-privileged port
with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    s.bind((HOST, PORT))
    s.listen(1)
    conn, addr = s.accept()
    with conn:
        print('Connected by', addr)
        while True:
            #data = conn.recv(1024)
            data1 = conn.send(b'Welcome')
            print(data.decode("utf-8"))
            if not data: break
            conn.sendall(data)
            print(data.decode("utf-8"))