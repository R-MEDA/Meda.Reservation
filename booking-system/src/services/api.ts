import { ApiRoot, RootKeys } from '@/api-resources/ApiRoot';
import { HalResource } from '@/api-resources/HalResource';

const API_BASE = 'http://localhost:5172/api';

export class ApiService {
    private static rootPromise: Promise<ApiRoot> | null = null;

    private static async getRoot(): Promise<ApiRoot> {
        if (!this.rootPromise) {
            this.rootPromise = this.get<ApiRoot>('');
        }
        return this.rootPromise;
    }

    static async followLink<T extends HalResource>(rel: RootKeys): Promise<T> {
        const root = await this.getRoot();
        const link = root._links[rel];
        if (!link) {
            throw new Error(`Link relation '${rel}' not found in API root`);
        }
        return this.get<T>(link.href);
    }

    static async get<T extends HalResource>(url: string): Promise<T> {
        const response = await fetch(url.startsWith('http') ? url : `${API_BASE}${url}`, {
            headers: {
                'Accept': 'application/json'
            }
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        return response.json();
    }

    static async post<T>(url: string, data?: any): Promise<T> {
        const response = await fetch(url.startsWith('http') ? url : `${API_BASE}${url}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            },
            body: data ? JSON.stringify(data) : undefined
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        return response.json();
    }
}
