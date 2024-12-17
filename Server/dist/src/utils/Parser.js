"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ParseSocketMessage = ParseSocketMessage;
function ParseSocketMessage(jsonString, parser) {
    const parsedData = JSON.parse(jsonString);
    const data = parser.parse(parsedData);
    return data;
}
//# sourceMappingURL=Parser.js.map