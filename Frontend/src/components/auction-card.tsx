import { Link } from "react-router-dom";
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';

interface AuctionCardProps
{
    auction: any;
}

const AuctionCard = ({ auction }: AuctionCardProps) => {

    const SetAuctionType = () => {
        switch(auction.typeId)
        {
            case 1:
                return <>This is a single item auction.</>
            case 2:
                return <>This auction has {auction.itemCount} items on auction presented one at a time.</>
            case 3:
                return <>This auction has {auction.itemCount} items on auction all items available for bidding.</>
            default:
                return <>Unknown auction details.</>
        }
    }

    const SetVisibility = () => {
        return auction.visibilityId === 1 ? <>Bind participant auction.</> : <>Public participant auction.</>
    }

    return (
        <Card sx={{ width: '100%', marginBottom: 2 }}>
            <CardContent>
                <Typography variant="h6" component="div">
                    <Link to={`/participate/${auction.id}`}>{auction.name}</Link>
                </Typography>
                <Typography sx={{ mb: 1.5 }} color="text.secondary">
                    Description: <SetAuctionType />{' '}<SetVisibility />
                </Typography>
                <Typography variant="body2">
                    <i className="fa-solid fa-stopwatch-20" style={{ marginRight: 3 }}></i> {auction.timeRemaining}
                </Typography>
            </CardContent>
        </Card>
    );
}

export default AuctionCard;