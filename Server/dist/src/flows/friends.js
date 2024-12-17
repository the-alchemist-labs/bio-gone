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
exports.getFriends = getFriends;
exports.getPendingFriendRequests = getPendingFriendRequests;
exports.sendFriendRequest = sendFriendRequest;
exports.handleFriendRequestRespond = handleFriendRequestRespond;
exports.emitPlayerOffline = emitPlayerOffline;
const FriendRequest_1 = require("../types/FriendRequest");
const clients_1 = require("../sockets/clients");
const PlayersStore = __importStar(require("../stores/PlayerStore"));
const FriendRequestStore = __importStar(require("../stores/FriendRequestStore"));
const FriendshipStore = __importStar(require("../stores/FriendshipStore"));
const Common_1 = require("../types/Common");
function getFriends(playerId) {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            const friendships = yield FriendshipStore.GetFriends(playerId);
            const friendIds = friendships.map(fs => getFriendId(playerId, fs));
            const friends = yield Promise.all(friendIds.map(getPlayerInfo));
            emitPlayerOnline(playerId, friendIds, true);
            return friends;
        }
        catch (error) {
            console.error(`Failed to get friends for ${playerId}`);
            return [];
        }
    });
}
function getPendingFriendRequests(recipient) {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            const requests = yield FriendRequestStore.GetFriendRequestsByRecipient(recipient);
            const players = yield Promise.all(requests.map(r => getPlayerInfo(r.sender)));
            return players;
        }
        catch (error) {
            console.error(`Failed to get friends requests for ${recipient}`);
            return [];
        }
    });
}
function sendFriendRequest(senderId, toFriendCode) {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            const code = formatFriendCode(toFriendCode);
            const recipient = yield PlayersStore.GetPlayerByFriendCode(code);
            if (!recipient) {
                console.log(`Friend ${toFriendCode} not found`);
                return { status: Common_1.Status.Failed, message: "Player not found" };
            }
            const isFriendRequestExists = yield FriendRequestStore.IsFriendRequestExists(senderId, recipient.id);
            if (isFriendRequestExists) {
                return { status: Common_1.Status.Failed, message: "You sent a request already" };
            }
            const isFriendshipExistes = yield FriendshipStore.IsFriendshipExists(senderId, recipient.id);
            if (isFriendshipExistes) {
                return { status: Common_1.Status.Failed, message: "You are already friends" };
            }
            yield FriendRequestStore.CreateFriendRequest({ sender: senderId, recipient: recipient.id });
            const recipientSocketId = clients_1.ClientManager.getClient(recipient.id);
            const sender = yield PlayersStore.GetPlayerById(senderId);
            if (recipientSocketId && sender) {
                // io.to(recipientSocketId).emit(SocketResponseEvent.FriendRequestReceived, { from: sender });
            }
            console.log('Friend request succesfully sent');
            return { status: Common_1.Status.Success };
        }
        catch (error) {
            console.error('Failed to send friend request', error);
            return { status: Common_1.Status.Failed, message: error.message };
        }
    });
}
function handleFriendRequestRespond(fromPlayerId, requestFromPlayerId, respond) {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            if (respond == FriendRequest_1.FriendRequestRespond.Accept) {
                yield FriendshipStore.CreateFriendRequest({ playerId1: fromPlayerId, playerId2: requestFromPlayerId });
                const recipientSocketId = clients_1.ClientManager.getClient(requestFromPlayerId);
                if (respond == FriendRequest_1.FriendRequestRespond.Accept && recipientSocketId) {
                    // io.to(recipientSocketId).emit(SocketResponseEvent.FriendRequestAccepted, { fromPlayerId });
                }
            }
            yield FriendRequestStore.DeleteFriendRequest({ sender: requestFromPlayerId, recipient: fromPlayerId });
            console.log("Friend request handled succesfuly", JSON.stringify({ fromPlayerId, requestFromPlayerId, respond }));
            return { status: Common_1.Status.Success };
        }
        catch (error) {
            console.error('Failed to get handle friend request responsd', JSON.stringify({ fromPlayerId, requestFromPlayerId, respond }));
            return { status: Common_1.Status.Failed, message: error.message };
        }
    });
}
function emitPlayerOffline(playerId) {
    return __awaiter(this, void 0, void 0, function* () {
        const friends = yield getFriends(playerId);
        const friendIds = friends.filter(Boolean).map(f => f.id);
        emitPlayerOnline(playerId, friendIds, false);
    });
}
function getFriendId(playerId, friendish) {
    const friendId = playerId === friendish.playerId1 ? friendish.playerId2 : friendish.playerId1;
    return friendId;
}
function getPlayerInfo(playerId) {
    return __awaiter(this, void 0, void 0, function* () {
        const player = yield PlayersStore.GetPlayerById(playerId);
        if (!player) {
            return null;
        }
        const isOnline = !!clients_1.ClientManager.getClient(playerId);
        return Object.assign(Object.assign({}, player), { isOnline });
    });
}
function formatFriendCode(code) {
    if (code.startsWith('#')) {
        code = code.slice(1);
    }
    return code.toUpperCase();
}
function emitPlayerOnline(playerId, friends, isOnline) {
    for (const friend of friends) {
        const friendSocketId = clients_1.ClientManager.getClient(friend);
        if (friendSocketId) {
            // io.to(friendSocketId).emit(SocketResponseEvent.FriendOnlineStatus, { playerId, isOnline });
        }
    }
}
//# sourceMappingURL=friends.js.map