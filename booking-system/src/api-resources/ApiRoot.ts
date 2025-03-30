import { HalResource } from "./HalResource";

export interface ApiRoot extends HalResource {
    _links : {
        self: { href: string };
        'available-slots': { href: string };
        'create-slot': { href: string; method: 'POST' };
    }
}

export type RootKeys = "self" | "slots" | "bookings";