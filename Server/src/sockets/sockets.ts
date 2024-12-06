
import { Server } from 'socket.io';
import { CommandMessage, SocketEvent } from '../types/Sockets';
import { searchMatch, postCommand } from '../flows/game-session';


export function initializeSockets(io: Server) {
  io.on(SocketEvent.Connection, async socket => {

    try {
      // Add schema validations
      socket.on(SocketEvent.SearchMatch, (data: string) => {
        searchMatch(data, socket);

      });
      socket.on(SocketEvent.PostCommand, (data: string) => postCommand(io, data));
      socket.on(SocketEvent.Disconnect, () => console.log("Socket disconected - ", socket.id));

    } catch (err) {
      socket.emit(SocketEvent.Error, { message: (err as Error).message });
      console.log("Socket event failed", (err as Error).message);
    }
  });
};
