
import { Server } from 'socket.io';
import { SocketEvent } from '../types/Sockets';
import { searchMatch, postCommand, removePlayerFromLobby } from '../flows/game-session';
import { socketHandler } from '../utils/middlewares';


export function initializeSockets(io: Server) {
  io.on(SocketEvent.Connection, async socket => {
    console.log("Socket connected - ", socket.id);

    // Add schema validations

    socketHandler(socket, SocketEvent.SearchMatch, (message: string) => searchMatch(message, socket));
    socketHandler(socket, SocketEvent.PostCommand, (message: string) => postCommand(io, message));
    socketHandler(socket, SocketEvent.Disconnect, (_message: string) => removePlayerFromLobby(socket.id));
  });
};
