import { ApiRoot, RootKeys } from '@/api-resources/ApiRoot';
import { HalResource } from '@/api-resources/HalResource';

const API_BASE = 'http://localhost:5172/api';

export class ApiService {
    private static rootPromise: Promise<ApiRoot> | null = null;

    private static async getRoot() {
        if (!this.rootPromise) {
            this.rootPromise = this.get('');
        }
        return this.rootPromise;
    }

    static async followLink<T extends HalResource>(rel: RootKeys): Promise<T> {
        const root = await this.getRoot();
        const link = root._links.find(link => link.rel === rel);
        if (!link) throw new Error(`Link '${rel}' not found`);
        return this.get<T>(link.href);
    }

    static async get<T>(url: string): Promise<T> {
        const response = await this.fetch(url);
        return response.json();
    }

    static async post<T>(url: string, data?: unknown): Promise<T> {
        const response = await this.fetch(url, {
            method: 'POST',
            body: data ? JSON.stringify(data) : undefined
        });
        
        // Return empty object for 204 No Content responses
        if (response.status === 204) {
            return {} as T;
        }

        // Check if there's actually content to parse
        const text = await response.text();
        if (!text) {
            return {} as T;
        }

        try {
            return JSON.parse(text);
        } catch (err) {
            console.error('API Response parsing error:', err, 'Response text:', text);
            throw new Error('Failed to parse server response');
        }
    }

    static async delete(url: string): Promise<void> {
        await this.fetch(url, { method: 'DELETE' });
    }

    private static async fetch(url: string, init?: RequestInit): Promise<Response> {
        const fullUrl = url.startsWith('http') ? url : `${API_BASE}${url}`;
        
        try {
            const response = await fetch(fullUrl, {
                ...init,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    ...init?.headers
                }
            });

            if (!response.ok) {
                const errorText = await response.text();
                console.error('API Error:', {
                    status: response.status,
                    statusText: response.statusText,
                    body: errorText
                });
                throw new Error(`Request failed: ${response.statusText}`);
            }

            return response;
        } catch (err) {
            console.error('Network error:', err);
            throw new Error('Network error occurred');
        }
    }
}
