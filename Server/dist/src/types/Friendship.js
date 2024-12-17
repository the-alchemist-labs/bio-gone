"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.friendshipSchema = void 0;
const zod_1 = require("zod");
exports.friendshipSchema = zod_1.z.object({
    playerId1: zod_1.z.string(),
    playerId2: zod_1.z.string(),
    createdAt: zod_1.z.date().optional(),
});
//# sourceMappingURL=Friendship.js.map