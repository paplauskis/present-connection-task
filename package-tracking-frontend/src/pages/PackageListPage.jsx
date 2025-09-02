import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { getAllPackages, filterPackages, getPackageByTracking } from '../apiCalls/packageApi'
import { usePackageStatus } from '../helpers/usePackageStatus'
import PackageTable from '../components/packageTable/PackageTable'
import TrackingNumberSearchForm from '../components/packageForm/TrackingNumberSearchForm'
import FilterSearchForm from '../components/packageForm/FilterSearchForm'

export default function PackageListPage() {
  const [packages, setPackages] = useState([])
  const [error, setError] = useState('')
  const [trackingNumber, setTrackingNumber] = useState('')
  const [statusFilter, setStatusFilter] = useState('')
  const [senderFilter, setSenderFilter] = useState('')
  const [recipientFilter, setRecipientFilter] = useState('')
  const navigate = useNavigate()
  const { changeStatus } = usePackageStatus(packages, setPackages)

  useEffect(() => {
    fetchPackages()
  }, [])

  const fetchPackages = async () => {
    const data = await getAllPackages()
    if (data.error) {
      setError(data.error)
      setPackages([])
    } else {
      setPackages(data)
      setError('')
    }
  }

  const handleSearchByTrackingNumber = async (e) => {
    e.preventDefault()
    if (!trackingNumber) {
      fetchPackages()
      return
    }

    const data = await getPackageByTracking(trackingNumber)
    if (data.error) {
      setError(data.error)
      setPackages([])
    } else {
      setPackages([data])
      setError('')
    }
  }

  const handleSearchByFilters = async (e) => {
    e.preventDefault()

    if (!statusFilter && !senderFilter && !recipientFilter) {
      fetchPackages()
      return
    }

    const data = await filterPackages({
      Status: statusFilter,
      SenderName: senderFilter,
      RecipientName: recipientFilter
    })

    if (data.error) {
      setError(data.error)
      setPackages([])
    } else {
      setPackages(data)
      setError('')
    }
  }

  return (
    <div>
      <h1>All Packages</h1>
      <button onClick={() => navigate('/create')}>Create new package</button>

      <TrackingNumberSearchForm
        trackingNumber={trackingNumber}
        setTrackingNumber={setTrackingNumber}
        onSubmit={handleSearchByTrackingNumber}
        onReset={() => {
          setTrackingNumber('')
          fetchPackages()
        }}
      />

      <FilterSearchForm
        statusFilter={statusFilter}
        senderFilter={senderFilter}
        recipientFilter={recipientFilter}
        setStatusFilter={setStatusFilter}
        setSenderFilter={setSenderFilter}
        setRecipientFilter={setRecipientFilter}
        onSubmit={handleSearchByFilters}
        onReset={() => {
          setStatusFilter('');
          setSenderFilter('');
          setRecipientFilter('');
          fetchPackages();
        }}
      />

      {error 
      ? (<p className="error-msg">{error}</p>)
      : (<PackageTable packages={packages} onStatusChange={changeStatus} />)}
    </div>
  )
}
