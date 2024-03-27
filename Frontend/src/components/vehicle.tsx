import { Link } from "react-router-dom";
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';

interface VehicleProps
{
    id: string;
    year: string;
    make: string;
    model: string;
    trim: string;
    hasBid: boolean;
}

const Vehicle = ({ year, make, model, trim, hasBid }: VehicleProps) => {
    return (
        <Card sx={{ width: '100%', marginBottom: 2 }}>
            <CardContent>
                <Typography variant="h6" component="div">
                    {year} {make} {model} {trim}
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

export default Vehicle;