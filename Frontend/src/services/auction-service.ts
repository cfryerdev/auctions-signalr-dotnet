import { useQuery } from 'react-query';

const fetchPastAuctions = async () => {
    const response = await fetch('http://localhost:5135/api/auctions/past');
    return await response.json();
}

const fetchTodaysAuctions = async () => {
    const response = await fetch('http://localhost:5135/api/auctions/today');
    const data = await response.json() as unknown as any[];
    return data.filter(item => !item.isActive );
}

const fetchFutureAuctions = async () => {
    const response = await fetch('http://localhost:5135/api/auctions/future');
    return await response.json();
}

const fetchAuctionDetails = async (id: string) => {
    const response = await fetch(`http://localhost:5135/api/lookups/auctions/${id}`);
    return await response.json();
}

const fetchAuctionItems = async (id: string) => {
    const response = await fetch(`http://localhost:5135/api/lookups/auctions/${id}/items`);
    return await response.json();
}

export const usePastAuctions = () => {
    return useQuery('fetchPastAuctions', fetchPastAuctions);
};

export const useTodaysAuctions = () => {
    return useQuery('fetchTodaysAuctions', fetchTodaysAuctions);
};

export const useFutureAuctions = () => {
    return useQuery('fetchFutureAuctions', fetchFutureAuctions);
};

export const useAuctionDetails = (id: string) => {
    return useQuery('fetchAuctionDetails', () => { return fetchAuctionDetails(id) });
}

export const useAuctionItems = (id: string) => {
    return useQuery('fetchAuctionItems', () => { return fetchAuctionItems(id) });
}

export default {
    fetchPastAuctions,
    fetchTodaysAuctions,
    fetchFutureAuctions,
    fetchAuctionDetails,
    fetchAuctionItems
}