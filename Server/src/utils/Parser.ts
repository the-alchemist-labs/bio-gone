import { ZodSchema } from "zod";

export function ParseSocketMessage<T>(jsonString: string, parser: ZodSchema<T>): T {
    const parsedData = JSON.parse(jsonString);
    const data = parser.parse(parsedData);
    return data;
}