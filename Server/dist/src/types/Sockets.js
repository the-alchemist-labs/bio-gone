"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.CommandMessageData = exports.SearchMatchData = exports.SocketEvent = void 0;
const zod_1 = require("zod");
var SocketEvent;
(function (SocketEvent) {
    SocketEvent["Connection"] = "connection";
    SocketEvent["Disconnect"] = "disconnect";
    SocketEvent["SearchMatch"] = "searchMatch";
    SocketEvent["PostCommand"] = "postCommand";
    SocketEvent["MatchFound"] = "matchFound";
    SocketEvent["CommandReceived"] = "commandReceived";
    SocketEvent["Error"] = "socketError";
})(SocketEvent || (exports.SocketEvent = SocketEvent = {}));
exports.SearchMatchData = zod_1.z.object({
    PlayerId: zod_1.z.string(),
});
exports.CommandMessageData = zod_1.z.object({
    RoomId: zod_1.z.string(),
}).passthrough();
//# sourceMappingURL=Sockets.js.map