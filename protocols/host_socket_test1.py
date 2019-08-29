import socket

# Create a TCP/IP socket
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

# Bind the socket to the port
server_address = ('', 5000)
print('Starting up on {} port {}'.format(*server_address))
sock.bind(server_address)

# Listen for incoming connections
sock.listen(1)
data = ""

while True:
    # Wait for a connection
    print('waiting for a connection')
    connection, client_address = sock.accept()
    try:
        print('connection from', client_address)

        # Receive the data in small chunks and retransmit it
        while True:
            data = connection.recv(1024)
            print('received {!r}'.format(data.decode("utf-8")))
            if data.decode("utf-8") == "tip":
                print('sending data back to the client')
                string = "tip is checked"
                connection.sendall(string.encode())
            elif data.decode("utf-8") == "Rotate":
                print('sending data back to the client')
                string = "Rotating the chip holder"
                connection.sendall(string.encode())
            elif data.decode("utf-8") == "finish":
                print('sending data back to the client')
                string = "Closing Host"
                connection.sendall(string.encode())
                break
        break
        
    finally:
        # Clean up the connection
        print("Closing current connection")
        connection.close()