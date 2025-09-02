import { useEffect, useState } from 'react'
import { useParams, useNavigate } from 'react-router-dom'
import { getPackageByTracking } from '../apiCalls/packageApi'
import { useSinglePackageStatus } from '../helpers/useSinglePackageStatus'
import PackageInfo from '../components/packageDetails/PackageInfo'
import PersonInfo from '../components/packageDetails/PersonInfo'
import StatusHistory from '../components/packageDetails/StatusHistory'
import UpdateStatus from '../components/packageDetails/UpdateStatus'

export default function PackageDetailsPage() {
  const navigate = useNavigate()
  const [pkg, setPkg] = useState(null)
  const [loading, setLoading] = useState(true)
  const [errMessage, setErrMessage] = useState('')
  const { trackingNumber } = useParams()
  const { changeStatus } = useSinglePackageStatus(pkg, setPkg)

  useEffect(() => {
    fetchPackage()
  }, [trackingNumber])

  const fetchPackage = async () => {
    setLoading(true)
    const data = await getPackageByTracking(trackingNumber)

    if (data.error) {
      setErrMessage(data.error)
    } else {
      setPkg(data)
      setErrMessage('')
    }

    setLoading(false)
  }

  if (loading) return <p>Loading package details...</p>
  if (errMessage) return <p className="error-msg">{errMessage}</p>
  if (!pkg) return <p>{errMessage || 'Package not found.'}</p>

  return (
    <div className="package-details-page">
      <h1>Package {pkg.trackingNumber}</h1>
      {errMessage && <p className="message">{errMessage}</p>}
      <div className="package-detail">
        <PackageInfo pkg={pkg} />
        <PersonInfo person={pkg.sender} isSender={true} />
        <PersonInfo person={pkg.recipient} isSender={false} />
        <StatusHistory statusHistory={pkg.statusHistory} />
        <UpdateStatus pkg={pkg} changeStatus={changeStatus} />
      </div>

      <button onClick={() => navigate('/')}>Back to List</button>
    </div>
  )
}