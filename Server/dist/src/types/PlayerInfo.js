"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.playerSocketConnectionSchema = exports.playerInfoSchema = void 0;
const zod_1 = require("zod");
const Player_1 = require("./Player");
exports.playerInfoSchema = Player_1.playerSchema.extend({
    isOnline: zod_1.z.boolean(),
});
exports.playerSocketConnectionSchema = zod_1.z.object({
    playerId: zod_1.z.string(),
});
//# sourceMappingURL=PlayerInfo.js.map