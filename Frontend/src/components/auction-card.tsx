import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';

interface AuctionCardProps
{
    auction: any;
    hasBid: boolean;
}

const AuctionCard = ({ auction, hasBid }: AuctionCardProps) => {
    return (
        <Card sx={{ width: '100%', marginBottom: 2, borderLeft: hasBid ? '2ps solid #e2e2e2' : '' }}>
            <CardContent>
                <Typography variant="h6" component="div">
                    {auction.name}
                </Typography>
                <Typography sx={{ mb: 1.5 }} color="text.secondary">
                    Total Items: { auction.itemCount }
                </Typography>
                <Typography variant="body2">
                    <i className="fa-solid fa-stopwatch-20" style={{ marginRight: 3 }}></i> {auction.timeRemaining}
                </Typography>
            </CardContent>
            {hasBid && (
                <CardActions>
                    <Button size="small">Submit Bid</Button>
                </CardActions>
            )}
        </Card>
    );
}

export default AuctionCard;