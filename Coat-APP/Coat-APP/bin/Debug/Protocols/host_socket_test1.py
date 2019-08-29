import socket
import json
import os


def is_json(myjson):
  try:
    json_object = json.loads(myjson)
  except ValueError as e:
    return False
  return True

cwd = "C:/Users/Alveolix/Documents/GitHub/coat_app/Coat-APP/Coat-APP/Protocols"
# Create a TCP/IP socket
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

# Bind the socket to the port
server_address = ('', 5003)
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
            if is_json(data) == True:
                data_loaded = json.loads(data)
            else:
                data_loaded = data.decode("utf-8")
            print('received {!r}'.format(data_loaded))
            if "tip" in data_loaded:
                print('sending data back to the client')
                string = "tip is checked"
                connection.sendall(string.encode())
                current_tip = data_loaded["tip"]
                print(current_tip)
                parameters = {"Current":current_tip}
                with open(cwd +'/currenttip.json', 'w') as json_file:
                    json.dump(parameters, json_file, indent=4)
                    json_file.close()
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