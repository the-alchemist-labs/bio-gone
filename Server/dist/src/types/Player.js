"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.playerSocketConnectionSchema = exports.playerSchema = void 0;
const zod_1 = require("zod");
exports.playerSchema = zod_1.z.object({
    id: zod_1.z.string(),
    name: zod_1.z.string(),
    profilePicture: zod_1.z.number(),
});
exports.playerSocketConnectionSchema = zod_1.z.object({
    playerId: zod_1.z.string(),
});
//# sourceMappingURL=Player.js.map