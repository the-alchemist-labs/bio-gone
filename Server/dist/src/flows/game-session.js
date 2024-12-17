"use strict";
var __createBinding = (this && this.__createBinding) || (Object.create ? (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    var desc = Object.getOwnPropertyDescriptor(m, k);
    if (!desc || ("get" in desc ? !m.__esModule : desc.writable || desc.configurable)) {
      desc = { enumerable: true, get: function() { return m[k]; } };
    }
    Object.defineProperty(o, k2, desc);
}) : (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    o[k2] = m[k];
}));
var __setModuleDefault = (this && this.__setModuleDefault) || (Object.create ? (function(o, v) {
    Object.defineProperty(o, "default", { enumerable: true, value: v });
}) : function(o, v) {
    o["default"] = v;
});
var __importStar = (this && this.__importStar) || function (mod) {
    if (mod && mod.__esModule) return mod;
    var result = {};
    if (mod != null) for (var k in mod) if (k !== "default" && Object.prototype.hasOwnProperty.call(mod, k)) __createBinding(result, mod, k);
    __setModuleDefault(result, mod);
    return result;
};
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
exports.searchMatch = searchMatch;
exports.postCommand = postCommand;
exports.removePlayerFromLobby = removePlayerFromLobby;
const Sockets_1 = require("../types/Sockets");
const playersFlow = __importStar(require("../flows/players"));
const lobby = [];
function searchMatch(_a, socket_1) {
    return __awaiter(this, arguments, void 0, function* ({ PlayerId }, socket) {
        if (lobby.length > 0) {
            OpenRoom([lobby.shift(), { socket, PlayerId }]);
        }
        else {
            lobby.push({ PlayerId, socket });
            console.log(`Joined lobby - ${PlayerId}`);
        }
    });
}
function postCommand(io, commandMessage) {
    return __awaiter(this, void 0, void 0, function* () {
        io.to(commandMessage.RoomId).emit(Sockets_1.SocketEvent.CommandReceived, commandMessage);
    });
}
function removePlayerFromLobby(identifier) {
    return __awaiter(this, void 0, void 0, function* () {
        const index = lobby.findIndex(record => record.PlayerId === identifier || record.socket.id === identifier);
        if (index !== -1) {
            lobby.splice(index, 1);
            console.log(`Removed ${identifier} from the lobby`);
        }
    });
}
function OpenRoom(records) {
    return __awaiter(this, void 0, void 0, function* () {
        const playerIds = records.map(s => s.PlayerId);
        const roomId = playerIds.join('-');
        const playersData = yield getPlayersData(playerIds);
        records.forEach(s => {
            s.socket.join(roomId);
            s.socket.emit(Sockets_1.SocketEvent.MatchFound, {
                RoomId: roomId,
                PlayersData: playersData,
                FirstTurnPlayer: getFirstPlayerIndex(records.length - 1),
            });
        });
    });
}
function getFirstPlayerIndex(playersNum) {
    return Math.floor(Math.random() * playersNum);
}
function getPlayersData(playerIds) {
    return __awaiter(this, void 0, void 0, function* () {
        const players = yield Promise.all(playerIds.map(playersFlow.getPlayer));
        return players.map(p => ({
            Id: p === null || p === void 0 ? void 0 : p.id,
            Name: p === null || p === void 0 ? void 0 : p.name,
            ProfilePicture: p === null || p === void 0 ? void 0 : p.profilePicture,
        }));
    });
}
//# sourceMappingURL=game-session.js.map