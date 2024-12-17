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
exports.GetFriendRequestsByRecipient = GetFriendRequestsByRecipient;
exports.CreateFriendRequest = CreateFriendRequest;
exports.DeleteFriendRequest = DeleteFriendRequest;
exports.IsFriendRequestExists = IsFriendRequestExists;
const FriendRequest_1 = require("../models/FriendRequest");
function GetFriendRequestsByRecipient(playerId) {
    return __awaiter(this, void 0, void 0, function* () {
        return FriendRequest_1.FriendRequestModel.find({ recipient: playerId }, { _id: 0, __v: 0 }).lean();
    });
}
function CreateFriendRequest(request) {
    return __awaiter(this, void 0, void 0, function* () {
        return FriendRequest_1.FriendRequestModel.create(request);
    });
}
function DeleteFriendRequest(_a) {
    return __awaiter(this, arguments, void 0, function* ({ sender, recipient }) {
        yield FriendRequest_1.FriendRequestModel.deleteMany({ sender, recipient });
    });
}
function IsFriendRequestExists(playerId1, playerId2) {
    return __awaiter(this, void 0, void 0, function* () {
        const friendRequest = yield FriendRequest_1.FriendRequestModel.findOne({
            $or: [
                { sender: playerId1, recipient: playerId2 },
                { sender: playerId2, recipient: playerId1 }
            ]
        }, { _id: 0, __v: 0 }).lean();
        return !!friendRequest;
    });
}
//# sourceMappingURL=FriendRequestStore.js.map