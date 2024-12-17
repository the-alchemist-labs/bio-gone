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
Object.defineProperty(exports, "__esModule", { value: true });
exports.ClientManager = void 0;
const _ = __importStar(require("lodash"));
class ClientManager {
    static getClient(playerId) {
        return this.clients[playerId] || null;
    }
    static addClient(playerId, socketId) {
        this.clients[playerId] = socketId;
    }
    static removeClient(socketId) {
        const keyToRemove = _.findKey(this.clients, value => value === socketId);
        if (keyToRemove) {
            _.unset(this.clients, keyToRemove);
        }
    }
}
exports.ClientManager = ClientManager;
ClientManager.clients = {};
//# sourceMappingURL=clients.js.map