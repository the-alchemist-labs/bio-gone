"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.FriendRequestRespond = exports.friendRequestSchema = void 0;
const zod_1 = require("zod");
exports.friendRequestSchema = zod_1.z.object({
    sender: zod_1.z.string(),
    recipient: zod_1.z.string(),
    createdAt: zod_1.z.date().optional(),
});
var FriendRequestRespond;
(function (FriendRequestRespond) {
    FriendRequestRespond[FriendRequestRespond["Accept"] = 0] = "Accept";
    FriendRequestRespond[FriendRequestRespond["Reject"] = 1] = "Reject";
})(FriendRequestRespond || (exports.FriendRequestRespond = FriendRequestRespond = {}));
//# sourceMappingURL=FriendRequest.js.map