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
exports.GetPlayerById = GetPlayerById;
exports.CreatePlayer = CreatePlayer;
exports.UpdaterPlayer = UpdaterPlayer;
const Player_1 = require("../models/Player");
function GetPlayerById(id) {
    return __awaiter(this, void 0, void 0, function* () {
        return Player_1.PlayerModel.findOne({ id }, { _id: 0, __v: 0 }).lean();
    });
}
function CreatePlayer(player) {
    return __awaiter(this, void 0, void 0, function* () {
        return Player_1.PlayerModel.create(player);
    });
}
function UpdaterPlayer(player) {
    return __awaiter(this, void 0, void 0, function* () {
        yield Player_1.PlayerModel.updateOne({ id: player.id }, player);
    });
}
//# sourceMappingURL=PlayerStore.js.map