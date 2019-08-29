import socket

# Create a TCP/IP socket
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

# Connect the socket to the port where the server is listening
server_address = ('169.254.11.184', 5000)
print('connecting to {} port {}'.format(*server_address))
sock.connect(server_address)

try:

    # Send data
    message = "tip"
    print('sending {!r}'.format(message.encode()))
    sock.sendall(message.encode())
    data = sock.recv(1024)
    print('received {!r}'.format(data.decode("utf-8")))
    
finally:
    print("OK")
    hello = input("hello")
    
try:
    
    # Send data
    message = "Rotate"
    print('sending {!r}'.format(message))
    sock.sendall(message.encode())
    data = sock.recv(1024)
    print('received {!r}'.format(data.decode("utf-8")))

finally:
    print("OK")
try:
    
    # Send data
    message = "finish"
    print('sending {!r}'.format(message.encode()))
    sock.sendall(message.encode())
    data = sock.recv(1024)
    print('received {!r}'.format(data.decode("utf-8")))
    

finally:
    print('closing socket')
    sock.close()