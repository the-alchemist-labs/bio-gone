"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.initializeSockets = initializeSockets;
const Sockets_1 = require("../types/Sockets");
const game_session_1 = require("../flows/game-session");
const middlewares_1 = require("../utils/middlewares");
function initializeSockets(io) {
    io.on(Sockets_1.SocketEvent.Connection, (socket) => __awaiter(this, void 0, void 0, function* () {
        console.log("Socket connected - ", socket.id);
        // Add schema validations
        (0, middlewares_1.socketHandler)(socket, Sockets_1.SocketEvent.SearchMatch, (message) => (0, game_session_1.searchMatch)(message, socket));
        (0, middlewares_1.socketHandler)(socket, Sockets_1.SocketEvent.PostCommand, (message) => (0, game_session_1.postCommand)(io, message));
        (0, middlewares_1.socketHandler)(socket, Sockets_1.SocketEvent.Disconnect, (_message) => (0, game_session_1.removePlayerFromLobby)(socket.id));
    }));
}
;
//# sourceMappingURL=sockets.js.map