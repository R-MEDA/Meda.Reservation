export interface Link {
    href: string;
    rel: string;
    method: string;
}

export interface HalResource {
    _links: Link[];
}
