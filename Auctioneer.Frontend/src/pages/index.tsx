import { Link } from "react-router-dom";

const IndexPage = () => {
  return (
    <>
      Welcome, check out the <Link to={'/list'}>List Page</Link>
    </>
  )
}

export default IndexPage;