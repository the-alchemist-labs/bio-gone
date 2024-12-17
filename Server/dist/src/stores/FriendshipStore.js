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
exports.GetFriends = GetFriends;
exports.CreateFriendRequest = CreateFriendRequest;
exports.DeleteFriendship = DeleteFriendship;
exports.IsFriendshipExists = IsFriendshipExists;
const Friendship_1 = require("../models/Friendship");
function GetFriends(playerId) {
    return __awaiter(this, void 0, void 0, function* () {
        return Friendship_1.FriendshipModel.find({ $or: [{ playerId1: playerId }, { playerId2: playerId }] }, { _id: 0, __v: 0 }).lean();
    });
}
function CreateFriendRequest(friendship) {
    return __awaiter(this, void 0, void 0, function* () {
        return Friendship_1.FriendshipModel.create(friendship);
    });
}
function DeleteFriendship(_a) {
    return __awaiter(this, arguments, void 0, function* ({ playerId1, playerId2 }) {
        yield Friendship_1.FriendshipModel.deleteOne({ playerId1, playerId2 });
    });
}
function IsFriendshipExists(playerId1, playerId2) {
    return __awaiter(this, void 0, void 0, function* () {
        const friendship = yield Friendship_1.FriendshipModel.findOne({
            $or: [
                { playerId1: playerId1, playerId2: playerId2 },
                { playerId1: playerId2, playerId2: playerId1 }
            ]
        }, { _id: 0, __v: 0 }).lean();
        return !!friendship;
    });
}
//# sourceMappingURL=FriendshipStore.js.map