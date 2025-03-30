import { Link } from "./HalResource";

export interface ApiRoot {
    _links: Link[];
}

export type RootKeys = 'self' | 'slots' | 'reservations';