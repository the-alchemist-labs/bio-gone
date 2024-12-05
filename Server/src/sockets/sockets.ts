
import { Server } from 'socket.io';
import { CommandMessage, SearchMatchData, SocketEvent } from '../types/Sockets';
import { searchMatch, postCommand } from '../flows/game-session';

let onlineCounter = 0;

export function initializeSockets(io: Server) {
  io.on(SocketEvent.Connection, async socket => {
    onlineCounter++;

    try {
      // Add schema validations
      socket.on(SocketEvent.SearchMatch, (data: SearchMatchData) => searchMatch(data, socket));
      socket.on(SocketEvent.PostCommand, (data: CommandMessage) => postCommand(io, data));
      socket.on(SocketEvent.Disconnect, () => { onlineCounter-- });

    } catch (err) {
      socket.emit(SocketEvent.Error, { message: (err as Error).message });
      console.log("Socket event failed", (err as Error).message);
    }
  });
};
