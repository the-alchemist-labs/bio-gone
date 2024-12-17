
import { Server } from 'socket.io';
import { CommandMessageData, SearchMatchData, SocketEvent } from '../types/Sockets';
import { searchMatch, postCommand, removePlayerFromLobby } from '../flows/game-session';
import { socketHandler } from '../utils/middlewares';


export function initializeSockets(io: Server) {
  io.on(SocketEvent.Connection, async socket => {
    console.log("Socket connected - ", socket.id);

    // Add schema validations

    socketHandler<SearchMatchData>(socket, SocketEvent.SearchMatch, (message: SearchMatchData) => searchMatch(message, socket));
    socketHandler<CommandMessageData>(socket, SocketEvent.PostCommand, (message: CommandMessageData) => postCommand(io, message));
    socketHandler(socket, SocketEvent.Disconnect, (_message: string) => removePlayerFromLobby(socket.id));
  });
};
